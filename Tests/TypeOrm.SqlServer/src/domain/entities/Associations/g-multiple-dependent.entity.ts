import { G_RequiredCompositeNav } from './g-required-composite-nav.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('g_multiple_dependent')
export class G_MultipleDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  multipleDepAttr: string;

  @ManyToOne(() => G_RequiredCompositeNav, (g_RequiredCompositeNav) => g_RequiredCompositeNav.g_MultipleDependents, { cascade: ['insert', 'update'], nullable: false, onDelete: 'CASCADE', orphanedRowAction: 'delete' })
  g_RequiredCompositeNav: G_RequiredCompositeNav;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}