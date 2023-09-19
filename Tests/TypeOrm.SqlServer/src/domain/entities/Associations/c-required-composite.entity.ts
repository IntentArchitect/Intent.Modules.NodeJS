import { C_MultipleDependent } from './c-multiple-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('c_required_composite')
export class C_RequiredComposite {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  requiredCompAttr: string;

  @ManyToOne(() => C_MultipleDependent, (c_MultipleDependent) => c_MultipleDependent.c_RequiredComposites, { cascade: ['insert', 'update'], nullable: false, onDelete: 'CASCADE', orphanedRowAction: 'delete' })
  c_MultipleDependent: C_MultipleDependent;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}