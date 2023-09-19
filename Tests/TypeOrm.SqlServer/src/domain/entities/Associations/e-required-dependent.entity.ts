import { E_RequiredCompositeNav } from './e-required-composite-nav.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToOne } from 'typeorm';

@Entity('e_required_dependent')
export class E_RequiredDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  requiredDepAttr: string;

  @OneToOne(() => E_RequiredCompositeNav, e_RequiredCompositeNav => e_RequiredCompositeNav.e_RequiredDependent, { cascade: ['insert', 'update'], nullable: false, onDelete: 'CASCADE', orphanedRowAction: 'delete' })
  e_RequiredCompositeNav: E_RequiredCompositeNav;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}