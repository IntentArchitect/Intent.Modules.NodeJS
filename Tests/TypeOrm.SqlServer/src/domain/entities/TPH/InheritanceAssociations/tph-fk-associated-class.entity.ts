import { TphFkDerivedClass } from './tph-fk-derived-class.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tph_fk_associated_class')
export class TphFkAssociatedClass {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  fkDerivedClassCompositeKeyA: string;

  @Column()
  fkDerivedClassCompositeKeyB: string;

  @ManyToOne(() => TphFkDerivedClass, { cascade: ['insert', 'update'], nullable: false })
  fkDerivedClass: TphFkDerivedClass;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}